using E_Nompilo_Healthcare_system.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Options;
using Microsoft.Extensions.Options;
using System.Data;
using System;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace E_Nompilo_Healthcare_system.UserClaimsIdentity
{
    public class SystemUserClaims : UserClaimsPrincipalFactory<HealthcareSystemUser, IdentityRole>
    {
        public SystemUserClaims(UserManager<HealthcareSystemUser> userManager, RoleManager<IdentityRole> roleManager, IOptions<IdentityOptions> options) : base(userManager, roleManager, options)
        {
        }

        public  async  Task<ClaimsIdentity> GetClaimsIdentityAsync(HealthcareSystemUser user)
        {
            var identity = await base.GenerateClaimsAsync(user);
            identity.AddClaim(new Claim("Gender", user.Gender ?? ""));
            identity.AddClaim(new Claim("LastName", user.LastName ?? ""));
            identity.AddClaim(new Claim("FirstName", user.FirstName?? ""));
            return identity;
        }
    }
}


  //< !--Header-- >
  //              < header class= "main-header " id = "header" >
  //                  < nav class= "navbar navbar-static-top navbar-expand-lg" >
  //                      < !--Sidebar toggle button -->
  //                      <button id="sidebar-toggler" class= "sidebar-toggle" >
  //                          < span class= "sr-only" > Toggle navigation </ span >
  //                      </ button >
  //                      < !--search form-- >
  //                      < div class= "search-form d-none d-lg-inline-block" >
  //                          < div class= "input-group" >
  //                              < button type = "button" name = "search" id = "search-btn" class= "btn btn-flat" >
  //                                  < i class= "mdi mdi-magnify" ></ i >
  //                              </ button >
  //                              < input type = "text" name = "query" id = "search-input" class= "form-control" placeholder = "search"
  //                             autofocus autocomplete = "off" />
  //                          </ div >
  //                          < div id="search-results-container">
  //                              <ul id="search-results"></ul>
  //                          </div>
  //                      </div>

  //                      <div class= "navbar-right " >
  //                          < ul class= "nav navbar-nav" >
  //                              < li class= "dropdown notifications-menu custom-dropdown" >
  //                                  < button class= "dropdown-toggle notify-toggler custom-dropdown-toggler" >
  //                                      < i class= "mdi mdi-bell-outline" ></ i >
  //                                  </ button >



  //                              < li class= "right-sidebar-in right-sidebar-2-menu" >
  //                                  < i class= "mdi mdi-settings mdi-spin" ></ i >
  //                              </ li >
  //                              < !--User Account-- >
  //                              < li class= "dropdown user-menu" >
  //                                  < button href = "#" class= "dropdown-toggle nav-link" data - toggle = "dropdown" >
  //                                      < img src = "" class= "user-image" alt = "User Image" />
  //                                      < span class= "d-none d-lg-inline-block" >< a class= "nav-link text-dark" asp - area = "Identity" asp - page = "/Account/Manage/Index" title = "Manage" > @role </ a ></ span >
  //                                  </ button >
  //                                  < ul class= "dropdown-menu dropdown-menu-right" >
  //                                      < !--User image-- >
  //                                      < li class= "dropdown-header" >
  //                                          < img src = "assets/img/user/user.png" class= "img-circle" alt = "User Image" />
  //                                          < div class= "d-inline-block" >
  //                                              < a class= "nav-link text-dark" asp - area = "Identity" asp - page = "/Account/Manage/Index" title = "Manage" > @role@*@User.Identity?.Name! *@</ a >
  //                                              < small class= "pt-1" ></ small >
  //                                           </ div >
  //                                      </ li >

  //                                      < li >
  //                                          < a href = "@*user-profile.html*@" >
  //                                              < i class= "mdi mdi-account" ></ i > My Profile
  //                                          </ a >
  //                                      </ li >


  //                                      < li class= "right-sidebar-in" >
  //                                          < a href = "javascript:0" > < i class= "mdi mdi-settings" ></ i > Account </ a >
  //                                      </ li >

  //                                      < li class= "dropdown-footer" >

  //                                          < a asp - area = "" asp - controller = "Home" >< i class= "mdi mdi-logout" ></ i > Log Out </ a >
  //                                      </ li >
  //                                  </ ul >
  //                              </ li >
  //                          </ ul >
  //                      </ div >
  //                  </ nav >
  //              </ header >