#README
## Sprint 3
### Both search criteria
```
SELECT "b"."Id", "b"."Employees", "b"."Location", "b"."Name"
      FROM "Bakeries" AS "b"
      WHERE ((@__ToLower_0 = '') OR (instr(lower("b"."Name"), @__ToLower_0) > 0)) AND ((@__ToLower_1 = '') OR (instr(lower("b"."Location"), @__ToLower_1) > 0))
```

### First  search criteria
```
SELECT "b"."Id", "b"."Employees", "b"."Location", "b"."Name"
      FROM "Bakeries" AS "b"
      WHERE (@__ToLower_0 = '') OR (instr(lower("b"."Name"), @__ToLower_0) > 0)
```
### Second search criteria

```
 SELECT "b"."Id", "b"."Employees", "b"."Location", "b"."Name"
      FROM "Bakeries" AS "b"
      WHERE (@__ToLower_0 = '') OR (instr(lower("b"."Location"), @__ToLower_0) > 0)
```

## Sprint 6

### New publisher

#### Request
```
POST https://localhost:5001/api/Bakers HTTP/1.1
Accept: application/json
Content-Type: application/json

{"name": "Arthur", "income":2000, "birthDate":"1984-11-05"}
```


#### Response
```
HTTP/1.1 201 Created
Date: Wed, 29 Dec 2021 13:54:10 GMT
Content-Type: application/json; charset=utf-8
Server: Kestrel
Transfer-Encoding: chunked
Location: https://localhost:5001/Api/Bakers/5

{
"id": 5,
"bakery": null,
"name": "Arthur",
"birthDate": "1984-11-05T00:00:00",
"income": 2000
}
Response code: 201 (Created); Time: 237ms; Content length: 86 bytes
```

### Edit publisher (succes)
#### Request
```
PUT https://localhost:5001/api/Bakers/1 HTTP/1.1
Content-Type: application/json

{"id": 1, "name": "Bert", "birthDate":  "2002-01-03", "income": 2200}
```

#### Response
```
HTTP/1.1 200 OK
Date: Wed, 29 Dec 2021 13:49:58 GMT
Content-Type: application/json; charset=utf-8
Server: Kestrel
Transfer-Encoding: chunked

{
"id": 1,
"bakery": null,
"name": "Bert",
"birthDate": "2002-01-03T00:00:00",
"income": 2200
}
Response code: 200 (OK); Time: 192ms; Content length: 84 bytes
```

### Edit publisher (fail)
#### Request

```
PUT https://localhost:5001/api/Bakers/1 HTTP/1.1
Content-Type: application/json

{"id": 2, "name": "Bert", "birthDate":  "2002-01-03", "income": 2200}
```
#### Response

```
HTTP/1.1 409 Conflict
Date: Wed, 29 Dec 2021 13:59:27 GMT
Content-Type: application/problem+json; charset=utf-8
Server: Kestrel
Transfer-Encoding: chunked

{
  "type": "https://tools.ietf.org/html/rfc7231#section-6.5.8",
  "title": "Conflict",
  "status": 409,
  "traceId": "00-0cdf41913b6aff4d843298f0e2b1fa75-7153422a953d2746-00"
}
Response code: 409 (Conflict); Time: 196ms; Content length: 160 bytes
```
