# MyFlow
To run it localy two .ENV fieles need to be added to:
> /api
>> 
>> POSTGRES_HOST=postgres
>>
>> POSTGRES_PORT=5432
>>
>> POSTGRES_USER=admin
>>
>> POSTGRES_PASSWORD=password
>>
>> POSTGRES_DATABASE=postgres
>>
>> PGADMIN_PORT=6081
>>
>> PGADMIN_PASSWORD=password # Is needed for the login for the PG-Admin Interface
>>
>> PGADMIN_EMAIL=admin@admin.com # Is needed for the login for the PG-Admin Interface
>> 
>> GITHUB_CLIENT_ID= < Your GITHUB Client ID >
>>
>> GITHUB_CLIENT_SECRET= < Your GITHUB Client Secret >
>> 
>> REDIRECT_AFTER_LOGIN=http://localhost:8080/overview # redirected to this URL after success auth
>
> /client
>>
>> NUXT_PUBLIC_API_BASE= < Your local endpoint of your API >
>> NUXT_PUBLIC_GITHUB_CLIENT_ID= < Your GITHUB Client ID >

## Have Fun 