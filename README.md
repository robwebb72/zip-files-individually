# zip-files-individually

A quick little C# program to zip up each file in a folder (depending on the file's extension) in its own zip file.

Mainly used for zipping up rom collections for systems like RecalBox or RetroPie.

Usage
`ZipFiles <extension-list> <path>`

`<extension-list>` a comma seperated list of extensions, only a file with an extension in this list will be zipped
`<path>` folder where the files to be zipped are located