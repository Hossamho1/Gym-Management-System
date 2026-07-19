using GymRoute.DataAccess.Enum;
using System.ComponentModel.DataAnnotations;


namespace GymRoute.BusinessLogic.ViewModel.HealthRecord;

public class HealthRecordDetailsViewModel 
{
    public decimal Height { get; set; }

    public decimal Weight { get; set; }

 
    public string? BloodType { get; set; }

    public string? Notes { get; set; }
}