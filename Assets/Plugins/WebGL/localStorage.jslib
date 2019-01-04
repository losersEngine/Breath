var JSLib = {

mobileAndTabletCheck: function()
{
  var check = (typeof window.orientation !== "undefined") || (navigator.userAgent.indexOf('IEMobile') !== -1);
  console.log(check);
  return check;
}

};

mergeInto(LibraryManager.library, JSLib);