all:
	nant

clean:
	nant clean

install:
	nant install -D:install.prefix="$(prefix)"
