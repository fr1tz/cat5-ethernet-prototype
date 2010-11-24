//------------------------------------------------------------------------------
// Revenge Of The Cats: Ethernet
// Copyright (C) 2008, mEthLab Interactive
//------------------------------------------------------------------------------

//------------------------------------------------------------------------------
// Array support functions.
//------------------------------------------------------------------------------

function arrayGetValue(%array, %key)
{
	%array.moveFirst();
	%idx = %array.getIndexFromKey(%key);
	if(%idx == -1)
		return "";

	return %array.getValue(%idx);
}

function arrayChangeElement(%array, %key, %value)
{
	%array.moveFirst();
	%idx = %array.getIndexFromKey(%key);
	if(%idx != -1)
		%array.erase(%idx);

	%array.push_back(%key, %value);
}

function readLinesIntoArray(%fileName, %array)
{
	%file = new FileObject();
	%file.openForRead(%fileName);
	%line = 0;
	while(!%file.isEOF())
	{
		%line++;
		%array.push_back(%line, strreplace(%file.readLine(), "<br>", "\n"));
	}
	%file.delete();
}
