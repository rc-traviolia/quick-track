function SaveByteStream(fileName, fileContent) {
    var link = document.createElement('a');
    link.download = fileName;
    link.href = "data:application/octet-stream;base64," + encodeURIComponent(fileContent)
    document.body.appendChild(link);
    link.click();
    document.body.removeChild(link);
}
function SavePdf(fileName, fileBytes) {
    var link = document.createElement('a');
    link.download = fileName;
    link.href = "data:application/octet-stream;base64," + fileBytes;
    document.body.appendChild(link);
    link.click();
    document.body.removeChild(link);
}



function deprecatedSaveZipFolder() {
    var zip = new JSZip();

    // Add an top-level, arbitrary text file with contents
    zip.file("Hello.txt", "Hello World\n");

    // Generate a directory within the Zip file structure
    var internalDirectory = zip.folder("week1");

    // Add a file to the directory, in this case an image with data URI as contents
    internalDirectory.file("smile.gif", imgData, { base64: true });

    // Generate the zip file asynchronously
    zip.generateAsync({ type: "blob" })
        .then(function (content) {
            // Force down of the Zip file
            saveAs(content, "QuickTrackTest.zip");
        });
}

function FileSaveAs(fileName, fileContent) {
    var link = document.createElement('a');
    link.download = fileName;
    link.href = "data:text/plain;charset=utf-8," + encodeURIComponent(fileContent)
    document.body.appendChild(link);
    link.click();
    document.body.removeChild(link);
}
function PdfSaveAs(fileName, fileBytes) {
    var link = document.createElement('a');
    link.download = fileName;
    link.href = "data:application/octet-stream;base64," + fileBytes;
    document.body.appendChild(link);
    link.click();
    document.body.removeChild(link);
}