//! ScriptApp.Home.debug.js
//

(function() {
function executeScript() {

Type.registerNamespace('ScriptApp.Home');

////////////////////////////////////////////////////////////////////////////////
// ScriptApp.Home._homePage

ScriptApp.Home._homePage = function ScriptApp_Home__homePage() {
}


ScriptApp.Home._homePage.registerClass('ScriptApp.Home._homePage');
(function () {
    var testFile = new SystemEx.IO.FileInfo('/Test/This');
    alert(testFile.get_path());
    testFile.createNewFile();
    var $enum1 = ss.IEnumerator.getEnumerator(SystemEx.IO.FileInfo.root.listFiles());
    while ($enum1.moveNext()) {
        var file = $enum1.get_current();
        alert(file.get_path());
    }
    alert('Done');
})();

}
ss.loader.registerScript('ScriptApp.Home', ['Script.WebEx'], executeScript);
})();

//! This script was generated using Script# v0.6.1.0
