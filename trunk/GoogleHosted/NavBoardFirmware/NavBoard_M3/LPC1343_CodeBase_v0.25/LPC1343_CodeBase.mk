.PHONY: all
all:
	@cs-make clean
	@echo Executing Post Build commands ...
	@lpcrc firmware.bin
	@echo Done
