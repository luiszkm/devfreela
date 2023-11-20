

namespace DevFreela.E2ETest.Api.Project.Common;
public class ProjectTestDataGenerator
{
    public static IEnumerable<object[]> GetInvalidProjectInputs()
    {
        var fixture = new ProjectAPITestFixture();
        var invalidInputsList = new List<object[]>();
        var totalInvalidCases = 3;

        for (int i = 0; i < totalInvalidCases; i++)
        {
            switch (i % totalInvalidCases)
            {
                case 0:
                    var input = fixture.GetProjectInputModel();
                    input.Title = fixture.GetInvalidName();
                    invalidInputsList.Add(new object[]
                    {
                        input
                    });
                    break;
                case 1:
                    var input2 = fixture.GetProjectInputModel();
                    input2.Description = fixture.GetInvalidEmail();
                    invalidInputsList.Add(new object[]
                    {
                        input2
                    });
                    break;
                case 2:
                    var input3 = fixture.GetProjectInputModel();
                    input3.TotalCost = -100;
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
}
