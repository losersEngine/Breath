var JSLib = {

mobileAndTabletCheck: function()
{
  var check = (typeof window.orientation !== "undefined") || (navigator.userAgent.indexOf('IEMobile') !== -1);
  return check;
}

};

mergeInto(LibraryManager.library, JSLib);
