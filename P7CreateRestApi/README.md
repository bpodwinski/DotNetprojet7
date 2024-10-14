# PostTrades
OpenClassrooms Projet 7 : Rendez votre back-end .NET plus flexible avec une API REST

## Informations Générales
Rendez votre back-end .NET plus flexible avec une API REST

## Installation
1. Cloner le projet 
2. Modifier le fichier appsettings.json
	```
		"ConnectionStrings": {
	  "DefaultConnection": "Server=.;Database=P7Create;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True"
	},
	```

## Utilisation
Pour se connecter à l'API, vous pouvez vous connecter pour obtenir un token d'authentification sur la route suivante avec ces identifiants :
**Route :** `POST /auth/login`
```
{  
	"username":  "admin",  
	"password":  "Ls7N0U7tmZ48!"  
}
```
**Réponse :**
```
{  
	"token":  "token"
}
```

Pour accéder aux routes protégées, vous devez utiliser le token obtenu lors de votre connexion. Cliquez sur le bouton `Authorize` en haut de la documentation Swagger, puis entrez le token dans ce format : `Bearer <votre-token>`.