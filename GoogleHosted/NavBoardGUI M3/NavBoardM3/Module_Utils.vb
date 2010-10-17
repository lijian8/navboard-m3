
'/**************************************************************************/
'/*! 
'    @file     Module_Utils.vb
'    @author   M. Ryan (ryanmechatronics.com)
'    @date     8 Aug 2010
'    @version  0.10

'    @section DESCRIPTION

'    Open Source utility module for Navigation Board M3 GUI

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



Public Class SoundPlaying

    Public Declare Function PlaySound Lib "WINMM.DLL" Alias _
        "sndPlaySoundA" (ByVal lpszSoundName As String, ByVal uFlags As Long) As Long

    Public Enum PlaySoundFlags
        Sync = &H0 'Sync (everything stops until sound is done)
        Async = &H1 'Async playing (desired!)
        [Loop] = &H8 'Loop continuously
        NoStop = &H10   'If used, sound currently playing will not be stopped
        NoDefault = &H2 'If sound file isn't found, system default will play unless you use this
    End Enum

    Public Sub PlayAsyncSound(ByVal title As String)
        Try
            PlaySound(title, PlaySoundFlags.Async)
        Catch
            Debug.Print("Sound error!")
        End Try
    End Sub
End Class
