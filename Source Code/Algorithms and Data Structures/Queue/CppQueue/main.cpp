#include <iostream>
#include <queue>

int main(int argc, const char **argv)
{
	std::queue<int> q;

	for(int i = 0; i < 10; i++)
	{
		q.push(i);
	}

	while(!q.empty())
	{
		std::cout << q.front() << std::endl;
		q.pop();
	}
}