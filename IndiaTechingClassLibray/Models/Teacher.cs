﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using IndiaTechingClassLibray.Models;


namespace India_Teaching.Models
{
    public class Teacher 
    {
        public List<Teacher> Teachers { get; set; }

        public int TeacherID { get; set; }

        [DisplayName("Full Name")]
        [Required(ErrorMessage = "Full Name is required")]
        [StringLength(100, ErrorMessage = "Full Name can't be longer than 100 characters")]
        public string Fullname { get; set; }

        [DisplayName("Date Of Birth")]
        [Required(ErrorMessage = "Date of Birth is required")]
        [DataType(DataType.Date, ErrorMessage = "Please enter a valid date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime DateofBirth { get; set; } = DateTime.Now;


        [DisplayName("Mobile Number")]
        [Required(ErrorMessage = "Mobile Number is required")]
        [Phone(ErrorMessage = "Please enter a valid mobile number")]
        [StringLength(12, MinimumLength = 10, ErrorMessage = "Mobile Number must be exactly 10 digits")]
        public string MobileNumber { get; set; }


        [DisplayName("Address")]
        [Required(ErrorMessage = "Address is required")]
        [StringLength(250, ErrorMessage = "Address can't be longer than 250 characters")]
        [RegularExpression(@"^[a-zA-Z\-_@/]+$", ErrorMessage = "Address can only contain alphabets, hyphens (-), underscores (_), at signs (@), and forward slashes (/)")]
        public string Address { get; set; }


        [DisplayName("Qualification")]
        [Required(ErrorMessage = "Qualification is Required")]
        [StringLength(200, ErrorMessage = "Qualification can't be longer than 200 characters")]
        [RegularExpression(@"^[a-zA-Z\-_@/]+$", ErrorMessage = "Address can only contain alphabets, hyphens (-), underscores (_), at signs (@), and forward slashes (/)")]
        public string Qualification { get; set; }

        [DisplayName("Marital Status")]

        public string Married { get; set; }


        [DisplayName("Profile Pic")]
        [StringLength(200, ErrorMessage = "Profile Pic link can't be longer than 200 characters")]
        public string ProfileLink { get; set; }

        [DisplayName("Intro Video")]
        [StringLength(200, ErrorMessage = "Intro Video link can't be longer than 200 characters")]
        public string VideoLink { get; set; }

        [DisplayName("Skill Name")]
        public int SkillId { get; set; }
      //  public List<SelectListItem> Skills { get; set; }
        public List<int> SelectedSkillIds { get; set; }

        public List<Notes> lstNotes { get; set; }

        public List<Skill> lstSkills { get; set; }

        public string SkillNames { get; set; }

        public string NotesTitles { get; set; }

        public List<string> SelectedIdsString { get; set; }

        public bool IsActive { get; set; }

        public int SharePercentage { get; set; }

    }
}