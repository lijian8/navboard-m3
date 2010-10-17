'/**************************************************************************/
'/*! 
'    @file     Class_NavBoard.vb
'    @author   M. Ryan (ryanmechatronics.com)
'    @date     8 Aug 2010
'    @version  0.10

'    @section DESCRIPTION

'    Open Source Nav Board class module for Navigation Board M3 GUI

'    @section Example

'    @code 

'    @endcode

'    @section LICENSE

'    Software License Agreement 

'    Copyright (c) 2010, Ryan Mechatronics LLC
'    All rights reserved.

'    Redistribution and use in source and binary forms, with or without
'    modification, are permitted provided that the following conditions are met:
'    1. Redistributions of source code must retain the above copyright
'    notice, this list of conditions and the following disclaimer.
'    2. Redistributions in binary form must reproduce the above copyright
'    notice, this list of conditions and the following disclaimer in the
'    documentation and/or other materials provided with the distribution.
'    3. Neither the name of the copyright holders nor the
'    names of its contributors may be used to endorse or promote products
'    derived from this software without specific prior written permission.

'    THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS ''AS IS'' AND ANY
'    EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED
'    WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE
'    DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT HOLDER BE LIABLE FOR ANY
'    DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES
'    (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES;
'    LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND
'    ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT
'    (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS
'    SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
'*/
'/**************************************************************************/
Public Class Class_NavBoard

    '* Quaternion representation:
    '*
    '* q = w + xi + yj + zk or q = (w, v) where w is a scalar and
    '* v = (x, y, z) is a vector
    Structure QUATERNION_STRUCT
        Dim w As Single
        Dim x As Single
        Dim y As Single
        Dim z As Single
    End Structure
    Structure EULER_STRUCT
        Dim phi As Single 'Roll
        Dim theta As Single 'Pitch
        Dim psi As Single 'Yaw
    End Structure

    Structure MSGHEADER_STRUCT
        Dim header As Long
        Dim id As Byte
        Dim idcnt As UInt16
        Dim sampleindex As Byte
        Dim latitude As Single
        Dim longitude As Single
        Dim gpstime As Long
        Dim month As Byte
        Dim day As Byte
        Dim hour As Byte
        Dim minute As Byte
    End Structure
    Structure NAVMSGHEADER_STRUCT
        Dim Header As UInt16 '0xAEAE
        Dim Len As Byte
        Dim DeviceID As Byte
        Dim MsgID As Byte
    End Structure


    Structure NAVBOARD_IMU_FP_MSG 'MSG ID 2
        Dim header As NAVMSGHEADER_STRUCT
        Dim cputemp As Single
        Dim acc() As Single
        Dim rate() As Single
        Dim mag() As Single
        Dim spare() As Single
        'Dim gyrotemp As Single
        Dim gTimeCnts As Single
    End Structure
    Structure NAVBOARD_ATTITUDE_MSG 'MSG ID 3
        Dim header As NAVMSGHEADER_STRUCT
        Dim phi As Single
        Dim theta As Single
        Dim psi As Single
        Dim phi_dot As Single
        Dim theta_dot As Single
        Dim psi_dot As Single
        Dim quat As QUATERNION_STRUCT
        Dim qdot As QUATERNION_STRUCT
        Dim bit As Byte
        Dim config As Byte
    End Structure

    Structure vec3d
        Dim x As Single
        Dim y As Single
        Dim z As Single
    End Structure

    Public Sub Get_Euler_From_Quat(ByRef v As EULER_STRUCT, ByVal q As QUATERNION_STRUCT)
        Try
            Dim x As Single
            'Returns euler using values in q
            Dim sqw As Single = q.w * q.w
            Dim sqx As Single = q.x * q.x
            Dim sqy As Single = q.y * q.y
            Dim sqz As Single = q.z * q.z
            Dim norm As Single
            norm = Math.Sqrt(sqw + sqx + sqy + sqz)

            'Normalize the quat
            q.w = q.w / norm
            q.x = q.x / norm
            q.y = q.y / norm
            q.z = q.z / norm

            v.phi = Math.Atan2(2.0 * (q.w * q.x + q.y * q.z), (1 - 2 * (sqx + sqy)))
            If (v.phi < 0) Then v.phi = v.phi + 2 * Math.PI
            x = ((2.0 * (q.w * q.y - q.z * q.x)))
            If (x > 1.0) Then x = 1.0
            If (x < -1.0) Then x = -1.0
            If ((q.x * q.y + q.z * q.w) = 0.5) Then
                v.theta = 2 * Math.Atan2(q.x, q.w)
            ElseIf ((q.x * q.y + q.z * q.w) = -0.5) Then
                v.theta = -2 * Math.Atan2(q.x, q.w)
            Else
                v.theta = Math.Asin(x)
            End If
            v.psi = Math.Atan2(2.0 * (q.w * q.z + q.x * q.y), (1 - 2 * (sqy + sqz)))
            If (v.psi < 0) Then v.psi = v.psi + (2 * Math.PI)

        Catch ex As Exception
            Debug.Print(ex.Message)
        End Try
    End Sub





End Class
