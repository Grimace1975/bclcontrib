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
    
    window.atob('This is here');

    var testFile = new SystemEx.IO.FileInfo('/Test/This');
    alert(testFile.get_path());
})();

}
ss.loader.registerScript('ScriptApp.Home', ['Script.WebEx'], executeScript);
})();

//! This script was generated using Script# v0.6.1.0
