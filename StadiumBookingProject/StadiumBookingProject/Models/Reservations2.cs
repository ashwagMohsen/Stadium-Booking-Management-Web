
namespace StadiumBookingProject.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Reservations2
    {
        public int id { get; set; }
        [Required]
        [Display(Name = "التاريخ المختار")]
        public string date { get; set; }
        [Required]
        [Display(Name = "الاسم الكامل")]

        public string full_name { get; set; }
        [Required]
        [Display(Name = "الساعات المختارة")]

        public Nullable<int> hours { get; set; }
        [Required]
        [Display(Name = "العربون")]

        public string earnest { get; set; }
        [Required]
        [Display(Name = "المتبقي")]

        public string residual { get; set; }
        [Required]
        [Display(Name = "السعر كامل")]

        public string price { get; set; }
        [Required]
        [Display(Name = "الوقت المختار")]

        public int time { get; set; }
        [Required]
        [Display(Name = "الجوال")]

        public string mobile { get; set; }
        [Required]
        [Display(Name = "PayPal رقم")]

        public int Pay_Pal_Number { get; set; }
        [Required]
        [Display(Name = "VIP رقم")]

        public int VIP { get; set; }
        [Required]
        [Display(Name = "رقم الملعب")]

        public Nullable<int> Stadium_Id { get; set; }
        [Required]

        public Nullable<int> Acc_Id { get; set; }
        [Required]
        [Display(Name = "الخدمات")]

        public Nullable<int> Serv_Id { get; set; }
        [Required]
        [EmailAddress]
        [Display(Name = "حساب البريد الإلكتروني")]

        public string Email { get; set; }
        public virtual Accounts Accounts { get; set; }
        public virtual Stadiums Stadiums { get; set; }
        public virtual Services Services { get; set; }
    }
}
