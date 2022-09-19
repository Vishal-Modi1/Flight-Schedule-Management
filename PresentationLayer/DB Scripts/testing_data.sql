declare @i int = 1  
declare @total int = 5000  
  
while @i <= @total  
begin  
    insert into Users (FirstName, LastName,Email,RoleId) values (RIGHT('000000'+convert(varchar, @i),6), convert(varchar, @i%4),'Email@'+Convert(varchar,@i)+'.com',@i%10+1)  
    set @i += 1;  
end 


update users set FirstName= 'First'+FirstName, LastName= 'LastName'+LastName where id>0

exec dbo.GetUserList '143', 1, 5, 'UserRole', 'Asc'