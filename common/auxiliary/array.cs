//------------------------------------------------------------------------------
// Cat5
// Copyright notices are in the file named COPYING.
//------------------------------------------------------------------------------

//------------------------------------------------------------------------------
// Array support functions.
//------------------------------------------------------------------------------

function arrayGetValue(%array, %key)
{
	if(!isObject(%array)) return;

	%array.moveFirst();
	%idx = %array.getIndexFromKey(%key);
	if(%idx == -1)
		return "";

	return %array.getValue(%idx);
}

function arrayChangeElement(%array, %key, %value)
{
	if(!isObject(%array)) return;

	%array.moveFirst();
	%idx = %array.getIndexFromKey(%key);
	if(%idx != -1)
		%array.erase(%idx);

	%array.push_back(%key, %value);
}

function readLinesIntoArray(%fileName, %array)
{
	if(!isObject(%array)) return;

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
