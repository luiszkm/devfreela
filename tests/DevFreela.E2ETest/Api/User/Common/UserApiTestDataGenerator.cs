


namespace DevFreela.E2ETest.Api.User.Common;
public class UserApiTestDataGenerator
{
    public static IEnumerable<object[]> GetInvalidInputs()
    {
        var fixture = new UserAPITestFixture();
        var invalidInputsList = new List<object[]>();
        var totalInvalidCases = 3;

        for (int i = 0; i < totalInvalidCases; i++)
        {
            switch (i % totalInvalidCases)
            {
                case 0:
                    var input = fixture.GetUserInput();
                    input.Name = fixture.GetInvalidName();
                    invalidInputsList.Add(new object[]
                    {
                        input
                    });
                    break;
                case 1:
                    var input2 = fixture.GetUserInput();
                    input2.Email = fixture.GetInvalidEmail();
                    invalidInputsList.Add(new object[]
                    {
                        input2
                    });
                    break;
                case 2:
                    var input3 = fixture.GetUserInput();
                    input3.Password = fixture.GetInvalidPassword();
                    invalidInputsList.Add(new object[]
                    {
                        input3
                    });
                    break;

                default:
                    break;
            }
        }

        return invalidInputsList;

    }

    public static IEnumerable<object[]> GetInvalidUpdateInputs()
    {
        var fixture = new UserAPITestFixture();
        var invalidInputsList = new List<object[]>();
        var totalInvalidCases = 3;

        for (int i = 0; i < totalInvalidCases; i++)
        {
            switch (i % totalInvalidCases)
            {
                case 0:
                    var input = fixture.GetUpdateUserInput();
                    input.Email = fixture.GetInvalidName();
                    invalidInputsList.Add(new object[]
                    {
                        input
                    });
                    break;
                case 1:
                    var input2 = fixture.GetUpdateUserInput();
                    input2.Email = fixture.GetInvalidEmail();
                    invalidInputsList.Add(new object[]
                    {
                        input2
                    });
                    break;


                default:
                    break;
            }
        }

        return invalidInputsList;

    }
}
