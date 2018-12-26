mergeInto(LibraryManager.library, {

  Load: function () {
    let lvlJson = localStorage.getItem('LVL');
    var lvl = lvlJson.lvl || 0;
    return lvl;
  },

  Save: function (lvl) {
    let lvlSaved = this.Load();

    if (lvl > lvlSaved)
        localStorage.setItem('LVL', JSON.stringify( { lvl: lvl } ));
  },

});