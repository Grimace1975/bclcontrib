// ScriptApp.Home.js
(function(){function executeScript(){
Type.registerNamespace('ScriptApp.Home');ScriptApp.Home._HomePage=function(){}
ScriptApp.Home._HomePage.registerClass('ScriptApp.Home._HomePage');(function(){var $0=new SystemEx.IO.FileInfo('/Test/This');alert($0.get_path());$0.createNewFile();var $enum1=ss.IEnumerator.getEnumerator(SystemEx.IO.FileInfo.root.listFiles());while($enum1.moveNext()){var $1=$enum1.get_current();alert($1.get_path());}alert('Done');})();
}
ss.loader.registerScript('ScriptApp.Home',['Script.WebEx'],executeScript);})();// This script was generated using Script# v0.6.1.0
