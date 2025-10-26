Hi! So, in order to boot up this project there is couple thigs you need to do first:
1. clone this repo
2. install docker
3. when docker installed, run `docker compose up -d` in directory with `docker-compose.yml` file
4. When booted up, swagger can be accessed `http://localhost:5285/swagger`
5. Register, get token then put token into authorize in swagger or in authorization header
6. Enjoy

So, when you got access token and call getUsers, you will see all users but only your user will balance in the dto.
If you are admin, then for every user you created, you will see all info.
This concerns all get user info(You can't see balance of other clients unless you are admin)

P. S. I know that interview task requires writing tests, but I hate writing tests with every living cell in my body, so if this is a requirement to pass on, then unlucky))
Anyway, thanks a lot for this task, I had fun writing it)
