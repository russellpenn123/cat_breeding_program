Useful commands:

Local:

curl -s -X POST http://localhost:5000/api/Cats \
 -H "Content-Type: application/json" \
 -d '{"name":"Luna","age":2,"isMale":false,"furColourHex":"#ffffff"}'

curl -s http://localhost:5000/api/Cats \
 -H "Content-Type: application/json" -i

Azure:

curl -s -X POST https://cat-breeding-program-api-dbhvhjdqhrffhmbb.uksouth-01.azurewebsites.net/api/Cats \
 -H "Content-Type: application/json" \
 -d '{"name":"Luna","age":2,"isMale":false,"furColourHex":"#ffffff"}'

curl -s https://cat-breeding-program-api-dbhvhjdqhrffhmbb.uksouth-01.azurewebsites.net/api/Cats \
 -H "Content-Type: application/json" -i
