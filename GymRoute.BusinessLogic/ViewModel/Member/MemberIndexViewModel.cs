
namespace GymRoute.BusinessLogic.ViewModel.Member;
public class MemberIndexViewModel
{
  public  int Id { get; set; }
    public string Name { get; set; } = null!;
    public string? Phone { get; set; }
    public string? Email { get; set; }
    public string? PhotoUrl { get; set; }
    public DateTime JoinDate { get; set; }
    public string Gender { get; set; } = null!;



}
