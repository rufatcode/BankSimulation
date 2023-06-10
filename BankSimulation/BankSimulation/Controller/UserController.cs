using System;
namespace BankSimulation.Controller
{
	public class UserController
	{
		public UserController()
		{
		}
	}
    enum UserChoice
    {
        CreateUser=10,
        DeleteUser,
        UpdateUser,
        GetAllUser,
        GetUserById,
        GetUserByName,
        CashIn,
        CachOut,
        SendMoneyToUser,
        GetUserByCartNumbers
    }
}

