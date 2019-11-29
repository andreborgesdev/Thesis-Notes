#include <iostream>
#include <sstream>
#include <stack>

bool TryParseInt (int &i, char const *s);

int main(int argc, const char** argv)
{
	std::stack<int> values;

	for(int i = 0; i < argc; i++)
	{
		int nextValue = 0;
		if(TryParseInt(nextValue, argv[i]))
		{
			values.push(nextValue);
		}
		else
		{
			if(std::strlen(argv[i]) == 1)
			{
				int rhs = values.top();
				values.pop();

				int lhs = values.top();
				values.pop();

				switch(argv[i][0])
				{
                        case '+':
                            values.push(lhs + rhs);
                            break;
                        case '-':
                            values.push(lhs - rhs);
                            break;
                        case '*':
                            values.push(lhs * rhs);
                            break;
                        case '/':
                            values.push(lhs / rhs);
                            break;
                        case '%':
                            values.push(lhs % rhs);
                            break;
                        default:
                            std::cerr << "Unrecognized input: " << argv[i] << std::endl;
							return -1;
				}
			}
		}
	}

	int result = values.top();
	std::cout << result << std::endl;
}

bool TryParseInt (int &i, char const *s)
{
    std::stringstream input(s);
    input >> i;

    char left;

	if (input.fail() || input.get(left)) {
        return false;
    }

    return true;
}