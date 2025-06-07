@echo off
setlocal enabledelayedexpansion
set API_URL=http://localhost:5167

echo.
echo === 1. LOGIN and get token ===
echo.

curl -s -X POST "%API_URL%/api/Auth/login" ^
  -H "Content-Type: application/json" ^
  -d "{\"email\":\"admin@localhost\",\"password\":\"Admin123!\"}" > login_response.json

echo Server response (raw JSON):
type login_response.json
echo.

set /p TOKEN="Paste the value of 'token' from the response above: "

echo.
echo === 2. GET /api/cars ===
curl -i -H "Authorization: Bearer %TOKEN%" "%API_URL%/api/cars"
echo.
echo.

echo === 3. POST /api/cars (create car) ===
curl -i -X POST "%API_URL%/api/cars" ^
  -H "Authorization: Bearer %TOKEN%" ^
  -H "Content-Type: application/json" ^
  -d "{\"brand\":\"TestBrand\",\"model\":\"TestModel\",\"year\":2024,\"price\":12345,\"sellerId\":1}"
echo.
echo.

echo === 4. PUT /api/cars/{id} (update car) ===
echo (Assuming the new car has id=1)
curl -i -X PUT "%API_URL%/api/cars/1" ^
  -H "Authorization: Bearer %TOKEN%" ^
  -H "Content-Type: application/json" ^
  -d "{\"id\":1,\"brand\":\"UpdatedBrand\",\"model\":\"UpdatedModel\",\"year\":2024,\"price\":54321,\"sellerId\":1}"
echo.
echo.

echo === 5. DELETE /api/cars/{id} (delete car) ===
curl -i -X DELETE "%API_URL%/api/cars/1" ^
  -H "Authorization: Bearer %TOKEN%"
echo   -> delete finished
echo.
echo.

echo === 6. GraphQL: get list of cars ===
curl -i -X POST "%API_URL%/graphql" ^
  -H "Content-Type: application/json" ^
  -d "{ \"query\": \"{ cars { id brand model year price } }\" }"
echo.
echo.
echo === TESTS FINISHED === 