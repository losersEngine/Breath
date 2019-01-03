var JSLib = {

<<<<<<< HEAD
Load: function()
{
  var lvlJson = localStorage.getItem('LVL');
  var lvl = lvlJson.lvl || -1;
  //SendMessage('GameManager','setLvl',lvl);
  return lvl;
},

Save: function(lvl)
{
  var lvlSaved = JSLib.Load();
  if (lvl > lvlSaved){
    localStorage.setItem('LVL', JSON.stringify( { "lvl": lvl } ));
  }
},

=======
>>>>>>> 5f4245200eaef8a1767dd87e4e8e72c87d15e4ce
mobileAndTabletCheck: function()
{
  var check = (typeof window.orientation !== "undefined") || (navigator.userAgent.indexOf('IEMobile') !== -1);
  return check;
}

};

mergeInto(LibraryManager.library, JSLib);
