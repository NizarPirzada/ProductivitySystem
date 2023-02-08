using ActivityLogs.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
namespace ActivityLogs
{

    public class ActivityLogService
    {


        private readonly UserManager<Microsoft.AspNetCore.Identity.IdentityUser> _userManager;
        private readonly ApplicationDbContext _db;
        private readonly SignInManager<Microsoft.AspNetCore.Identity.IdentityUser> _signInManager;
        private readonly RoleManager<Microsoft.AspNetCore.Identity.IdentityRole> _RoleManager;
        public int count { get; set; } = 0;
        public object HttpContext { get; private set; }

        public ActivityLogService(ApplicationDbContext db, UserManager<Microsoft.AspNetCore.Identity.IdentityUser> userManager, SignInManager<Microsoft.AspNetCore.Identity.IdentityUser> signInManager, RoleManager<Microsoft.AspNetCore.Identity.IdentityRole> roleManager)
        {
            _db = db;
            _userManager = userManager;
            _signInManager = signInManager;
            _RoleManager = roleManager;


        }
        #region Admin
        public async Task<List<ActivityLog>> GetActivityLog()
        {
            try
            {
                var UserId = _signInManager.Context.User.Claims.FirstOrDefault().Value;
                var user = await _userManager.FindByIdAsync(UserId);
                var role = await _userManager.GetRolesAsync(user);
                var roles = role.FirstOrDefault();
                if (roles == "Admin")
                {
                    ///*var results = */  return await _db.ActivityLogs.ToListAsync();
                    ///

                    ////return results;
                    List<ActivityLog> activitylogList = _db.ActivityLogs.OrderByDescending(x => x.ActivityId).ToList();
                    List<ActivityDto> activity = activitylogList.Select(activityModel => new ActivityDto()
                    {
                        Id = activityModel.Id,
                        ActivityDescription = activityModel.ActivityDescription,
                        Date = activityModel.Date,
                        HowIFeel = activityModel.HowIFeel,
                        Duration = activityModel.Duration,
                        Value = activityModel.Value,
                        ProjectId=activityModel.ProjectId
                       
                    }).ToList();
                    return activitylogList;
                }
                else
                {
                    //return await _db.ActivityLogs.Where(x => x.Id != "Admin").ToListAsync();
                    List<ActivityLog> activitylogList = _db.ActivityLogs.OrderByDescending(x=>x.ActivityId).Where(x=>x.Id!="Admin").ToList();
                    List<ActivityLogDTO> activity = activitylogList.Select(activityModel => new ActivityLogDTO()
                    {
                                           Id = activityModel.Id,
                                          ActivityDescription = activityModel.ActivityDescription,
                                           Date = activityModel.Date,
                                            HowIFeel = activityModel.HowIFeel,
                                            Duration = (decimal)activityModel.Duration,
                                            Value = activityModel.Value,
                                            ProjectId=activityModel.ProjectId
                                            
                    }).ToList();
                    return activitylogList;

                    //var bookings = (from booking in _db.ActivityLogs
                    //                where booking.Id != "0d46f8b2-dafe-414b-9c82-a43e732ca558"
                    //                join guest in _db.AspNetUsers on booking.Id equals guest.Id
                    //                select new ActivityLog
                    //                {
                    //                    Id=booking.Id,
                    //                    ActivityDescription = booking.ActivityDescription,
                    //                    Date = booking.Date,
                    //                    HowIFeel = booking.HowIFeel,
                    //                    Duration = booking.Duration,
                    //                    Value = booking.Value,
                    //                }).ToList();
                    //return bookings;              
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
       
          public List<Project> GetProject()
        {
            try
            {

                List<Project> activitylogList = _db.Projects.ToList();
                List<ActivityViewModel> activity = activitylogList.Select(activityModel => new ActivityViewModel()
                {
                    ProjectName = activityModel.ProjectName
                }).ToList();
                return activitylogList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Microsoft.AspNetCore.Identity.IdentityUser> GetUsers()
        {
            try
            {

                List<Microsoft.AspNetCore.Identity.IdentityUser> activitylogList = _db.AspNetUsers.ToList();
                List<ActivityViewModel> activity = activitylogList.Select(activityModel => new ActivityViewModel()
                {
                    UserName = activityModel.UserName
                }).ToList();
                return activitylogList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public string AddActivityLog(ActivityDto activity, string Id)
        {
            try
            {
                if (count == 1)
                {
                    count = 0;
                    return "duplicate";
                }
                else
                {
                    var act = new ActivityLog();
                    {
                        act.Id = activity.Id;
                        act.Date = activity.Date;
                        act.Duration = activity.Duration;
                        act.Value = activity.Value;
                        act.ActivityDescription = activity.ActivityDescription;
                        act.HowIFeel = activity.HowIFeel;
                        act.Id = activity.Id;
                        act.ProjectId = activity.ProjectId;
                        _db.ActivityLogs.Add(act);
                        _db.SaveChanges();
                      
                        return "Save Successfully";
                    }
                  
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }



        public bool EditActivityLog(ActivityLog activity)
        {
            try
            {
                ActivityLog activitylog = _db.ActivityLogs.FirstOrDefault(x => x.ActivityId == activity.ActivityId);
                if (activity != null)
                {
                    //emps.ID = emp.ID;
                    activitylog.Id = activity.Id;
                    activitylog.Date = activity.Date;
                    activitylog.ActivityDescription = activity.ActivityDescription;
                    activitylog.HowIFeel = activity.HowIFeel;
                    activitylog.Duration = activity.Duration;
                    activitylog.Value = activity.Value;
                    _db.ActivityLogs.Update(activity);
                    _db.SaveChanges();

                };
                return true;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        public ActivityLog ActivityLogDetails(int id)
        {
            try
            {
                var emp = _db.ActivityLogs.FirstOrDefault(x => x.ActivityId == id);
                return emp;
            }
            catch (Exception ex)
            {
                ex.ToString();
                throw ex;
            }
        }

        public async Task<List<ActivityLog>> Searchdate(ActivityViewModel date, string Id)
        {
            try
            {
                var UserId = _signInManager.Context.User.Claims.FirstOrDefault().Value;
                var user = await _userManager.FindByIdAsync(UserId);
                var role = await _userManager.GetRolesAsync(user);
                var roles = role.FirstOrDefault();
                List<ActivityLog> activitylogList = _db.ActivityLogs.Where(x => x.Date >= date.fromDate && x.Date <= date.ToDate || x.Id==Id).ToList();
                List<ActivityViewModel> activity = activitylogList.Select(activityModel => new ActivityViewModel()
                {
                    HowIFeel = activityModel.HowIFeel,
                    Duration = activityModel.Duration,
                    ActivityDescription = activityModel.ActivityDescription,
                    Value = activityModel.Value,
                    fromDate = date.fromDate,
                    ToDate = date.ToDate
                }).ToList();
                return activitylogList;

                //var res= _db.ActivityLogs.Where(x => x.Date >= date.fromDate && x.Date <= date.ToDate).ToList();

                //return res;


            }
            catch (Exception ex)
            {

                throw ex;
            }


        }
        public string DeleteActivity(int id)
        {
            try
            {
                var emp = _db.ActivityLogs.FirstOrDefault(x => x.ActivityId == id);
                _db.ActivityLogs.Remove(emp);
                _db.SaveChanges();
                return "SuccessFully";
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public List<ActivityLog> DropDownSearch(string Id)
        {

            try
            {



                //List<ActivityLog> bookings = (from activitys in _db.ActivityLogs
                //                              join aspnetuse in _db.AspNetUsers on activitys.Id equals aspnetuse.Id
                //                              select new ActivityLog
                //                              {
                //                                  ActivityId = activitys.ActivityId,
                //                                  ActivityDescription = activitys.ActivityDescription,
                //                                  HowIFeel = activitys.HowIFeel,
                //                                  Duration = activitys.Duration,
                //                                  Date = activitys.Date, 
                //                                  Value = activitys.Value
                //                              }).ToList();
                //return bookings;
                List<ActivityLog> activitylogList = _db.ActivityLogs.Where(x => x.Id == Id).ToList();
                List<ActivityViewModel> activity = activitylogList.Select(activityModel => new ActivityViewModel()
                {
                    HowIFeel = activityModel.HowIFeel,
                    Duration = activityModel.Duration,
                    ActivityDescription = activityModel.ActivityDescription,
                    Value = activityModel.Value,
                    Date = (DateTime)activityModel.Date
                }).ToList();
                return activitylogList;



            }
            catch (Exception ex)
            {

                throw ex;
            }



        }
        #endregion
        #region User
        public async Task<List<ActivityLog>> GetUserActivityLog()
        {



            try
            {

                var UserId = _signInManager.Context.User.Claims.FirstOrDefault().Value;
                var user = await _userManager.FindByIdAsync(UserId);
                var role = await _userManager.GetRolesAsync(user);
                var roles = role.FirstOrDefault();
                if (roles == "User")
                {
                    var result = _db.ActivityLogs.OrderByDescending(x=>x.ActivityId).Where(x => x.Id == UserId).ToList();
                    return result;
                }
                else
                {
                    return null;
                }





            }


            catch (Exception ex)
            {
                throw ex;
            }
        }
        public string AddUserActivityLog(ActivityLog activity,string projectId)
        {

            try
            {
                if (count == 1)
                {
                    count = 0;
                    return "duplicate";
                }
                else
                {
                    var UserId = _signInManager.Context.User.Claims.FirstOrDefault().Value;
                    var user = _userManager.FindByIdAsync(UserId);
                    activity.Id = user.Result.ToString();
                    projectId = activity.ProjectId;
                    _db.ActivityLogs.Add(activity);
                    _db.SaveChanges();
                    return "Save Successfully";
                }




            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public List<ActivityLog> SearchUserdate(ActivityViewModel date,string Id)
        {

            try
            {

                List<ActivityLog> activitylogList = _db.ActivityLogs.Where(x => x.Date >= date.fromDate && x.Date <= date.ToDate|| x.Id == Id).ToList();
                List<ActivityViewModel> activity = activitylogList.Select(activityModel => new ActivityViewModel()
                {
                    HowIFeel = activityModel.HowIFeel,
                    Duration = activityModel.Duration,
                    ActivityDescription = activityModel.ActivityDescription,
                    Value = activityModel.Value,
                    fromDate = date.fromDate,
                    ToDate = date.ToDate
                }).ToList();
                return activitylogList;

                //var res= _db.ActivityLogs.Where(x => x.Date >= date.fromDate && x.Date <= date.ToDate).ToList();

                //return res;


            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        public bool EditUserActivityLog(ActivityLog activity)
        {
            try
            {
                var UserId = _signInManager.Context.User.Claims.FirstOrDefault().Value;
                var user = _userManager.FindByIdAsync(UserId);

                if (user.Result.ToString() != activity.Id)
                {
                    return false;
                }


                else
                {
                    ActivityLog activitylog = _db.ActivityLogs.FirstOrDefault(x => x.ActivityId == activity.ActivityId);
                    activitylog.Date = activity.Date;
                    activitylog.ActivityDescription = activity.ActivityDescription;
                    activitylog.HowIFeel = activity.HowIFeel;
                    activitylog.Duration = activity.Duration;
                    activitylog.Value = activity.Value;
                    _db.ActivityLogs.Update(activity);
                    _db.SaveChanges();
                };
                return true;

            }

            catch (Exception ex)
            {

                throw ex;
            }

        }
        public ActivityLog UserActivityLogDetails(int id)
        {
            try
            {
                var emp = _db.ActivityLogs.FirstOrDefault(x => x.ActivityId == id);
                return emp;
            }
            catch (Exception ex)
            {
                ex.ToString();
                throw ex;
            }
        }
        public string UserDeleteActivityLog(int id, ActivityLog activity)
        {
            //var UserId = _signInManager.Context.User.Claims.FirstOrDefault().Value;
            try
            {
              
                    var emp = _db.ActivityLogs.FirstOrDefault(x => x.ActivityId == id);
                    _db.ActivityLogs.Remove(emp);
                    _db.SaveChanges();
                    return "SuccessFully";
               
               

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public List<Microsoft.AspNetCore.Identity.IdentityUser> GetsUsers()
        {
            try
            {

                List<Microsoft.AspNetCore.Identity.IdentityUser> activitylogList = _db.AspNetUsers.Where(x=>x.Id!= "0d46f8b2-dafe-414b-9c82-a43e732ca558").ToList();
                List<ActivityViewModel> activity = activitylogList.Select(activityModel => new ActivityViewModel()
                {
                    UserName = activityModel.UserName
                }).ToList();
                return activitylogList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
    }
}
