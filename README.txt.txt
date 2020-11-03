Projektelis veikia taip jog prisiloginus ir gavus Jwt Bearer tokena jau galima daryti sekancius requestus su Bearer headeriu 
i kitus endpointus kaip "api/v1/agreements", "api/v1/agreements/3", "api/v1/agreements/3/interests". 
Paskutinis paminetas endpointas taip pat gali tureti ir parametra, nauja base rate reiksme. 
Su kuria bus grazinamas ir esamas agreement interest rate ir naujas su nauja base rate reiksme.
Norejau daugiau padaryti, prideti galimybe pakeisti Agreement reiksme duombazeje, prideti nauja ir istrinti. 
Projektelis parasytas ant C# core, norejau bet nespejau perkelti and .Net (gali buti kad perkelineju kol jus skaitot).
Pradzioj naudojau Domain driven design, bet projektas mazas tai nusprendziau Accounts ir Agreements daliu neisskirstyti ir DDD nenaudoti.
Taip pat stengiausi naudoti single responsability principle, dependancy injection, repositories pattern, services/clients patterns.

Pridejau ir Photos folderi su duombazes ir postman nuotraukom.

Panaudotos "Depedencies":

1. AutoMapper.Extensions.Microsoft.DependencyInjection -> sumappinti panasius modelius,
   pvz kaip is duombazes ateinancius Entities ir vadinamus ViewModel arba DTOS kuriuos matys klientas.  
2. Microsoft.AspNetCore.Authentication.JwtBearer -> naudojau autentifikavimui pagal Bearer headeri.
3. Microsoft.AspNetCore.JsonPatch ir Microsoft.AspNetCore.Mvc.NewtonsoftJson -> nespejau implementuoti, bet butu naudojama Agreement updatinimui
4. Microsoft.EntityFrameworkCore, Microsoft.EntityFrameworkCore.Design ir Microsoft.EntityFrameworkCore.SqlServe -> naudojau susisiekimui su duombaze. 
   Populeri biblioteka, manau tinka ivairaus dydzio projektams

Panaudoti irankiai:

1. EF migrations -> naudota duombazei ir lentelem sukurti
2. PostMan -> na be sito neapseinama :)
