var JSLib = {

Load: function()
{
  var lvlJson = localStorage.getItem('LVL');
  var lvl = lvlJson.lvl || -1;
  //SendMessage('GameManager','setLvl',lvl);
  return lvl;
},

Save: function(lvl)
{
  var lvlSaved = this.Load();
  if (lvl > lvlSaved){
    localStorage.setItem('LVL', JSON.stringify( { "lvl": lvl } ));
  }
},

mobileAndTabletCheck: function()
{
  var check = (typeof window.orientation !== "undefined") || (navigator.userAgent.indexOf('IEMobile') !== -1);
  //SendMessage('GameManager', 'isMobile', check);
  return check;
}

};

mergeInto(LibraryManager.library, JSLib);
