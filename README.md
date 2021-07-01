# Message Service
MessageService is a **.NET Core 3.1** based service to handle messaging operations. 

## Table of Contents

- [Authentication](#authentication)
	- [Key Based Authentication](#key-based-authentication)
	- [Ip Based Authentication](#ip-based-authentication)
- [Data Models](#data)
- [Endpoints](#endpoints)
	- `POST`  [/message](#Add-Message)  `Adds new message` 
	- `DELETE`[/message](#delete-message)  `Deletes a message` 
	- `GET`   [/message](#get-message)  `Gets a message details` 
	- `GET`   [/message/basicinfo](#get-message-basic-info)  `Gets a message's basic info` 
	- `GET`   [/message/list](#message-list)  `Gets message List` 
	- `GET`   [/message/count](#message-count)  `Gets message count` 
	- `GET`   [/message/topsenders](#get-message-top-senders)  `Gets message top senders` 
	- `POST`  [/reply](#add-reply)  `Adds a reply` 
	- `GET`   [/reply](#reply-list)  `Gets reply list` 
	- `DELETE`[/reply](#delete-reply)  `Deletes the reply` 
	- `POST`  [/log](#add-log)  `Adds a log` 

## Authentication
MessageService has two authentication option. **Token based authentication** and **ip based authentication**. Keys and ip adresses are stored in **appsettings.json**, under the **Security** section.

Check the example authentication configuration given below.

```json
// appsettings.json 
// Auth Config Start 

  "Security": {
    "Keys": [
      "my-auth-key", 
      "my-other-key"
    ],
    "IpAddresses": [
      "123.456.789.11",
      "124.567.234.11"
    ]
  },
// Auth Config End 

```
### Key Based Authentication
To use key based authentication, first you need to add your keys to **appsettings.json** as above. Then, you need to add your key to **request header**.

#### Request Header With Authentication Key
```json
`GET /message/count?relatedId=1
Token: my-auth-key  //Here is our authentication key
Accept: */*
Host: localhost:44375
Accept-Encoding: gzip, deflate, br
Connection: keep-alive`
```

As you can see above, we passed our key to **token** section in our request. 

If request authention fails, service returns `401 Unauthorized` status code.

### Ip Based Authentication
As we see on the example config file in [Authentication](#authentication) section, Message Service  supports ip address authentication too.  All you need to do is add your ip addresses to **appsettings.json** file.

After adding ip addresses to configuration file, **service will only accept requests from configured ip addresses**.  Otherwise it will return `401 Unauthorized` status code.

## Data
Below you can see the **data model** behind the Message Service.

 ### Model Base 
- Id `int` 
- DateAdded `DateTime`
- IsDeleted `bool` 

### Message

- RelatedId `int` 
- FromId `int?` 
- ToId  `int?` 
- IpAddr `string`
- MessageContent `string`
- **Replies<>** `Reply`
- **Logs**<> `Log`

### Reply

- **MessageId** `int` 
- IsReply `bool`
- MessageContent `string` 
- IpAddr `string`

### Log

- Action `tinyint`
- **MessageId** `int`

## Endpoints

 ### Add Message<br>
`Path: /message/`  <br>
`Method: POST` <br>
`Content-Type: application/json` <br>
`Returns: The added message.` <br>
`Success Status Code: 201 Created` <br>

 -*Adds a new message.*

| Property  | Type | Default |Desc |
| ------------ | ------------ |------------  | ------------ |
| RelatedId  |        int   | | Related identity keys of your entities. |
| FromId  |     int | Optional  | Message sender id. |
| ToId | int  | Optional | Message receiver id. |
| MessageContent | string | | Content of the message. |
| IpAddr | string  | Request ip address | Ip address of the sender. |
------------

#### Request Body

```json
{
    "RelatedId" : 1,
    "FromId" : 1,
    "ToId" : 1,
    "MessageContent" : "Hello World",
    "IpAddr": "123.456.789"
}
```

#### Success Response
`Status Code: 201 Created`
```json
{
    "data": {
        "id": 7,
        "dateAdded": "2021-06-30T11:18:37.1691819Z",
        "relatedId": 1,
        "fromId": 1,
        "toId": 1,
        "ipAddr": "123.456.789",
        "messageContent": "Hello World",
        "replyCount": 0
    },
    "totalCount": 0,
    "success": true,
    "message": null,
    "exceptionType": null,
    "statusCode": 201,
    "messages": null,
    "modelStateErrors": null
}
```
------------

### Delete Message<br>
`Path: /message` <br>
`Method: DELETE` <br>
`Returns: Status Code` <br>
`Success Status Code: 200 OK` <br>

 -*Deletes message by messageId.*

| Parameter  | Type |  Default | Desc | 
| ------------ | ------------  | ------------ |  ------------ |
| id  |   int   |   | Message ID to delete. |


#### Success Response
`Status Code: 200 OK`
```json
{
    "success": true,
    "message": null,
    "exceptionType": null,
    "statusCode": 200,
    "messages": null,
    "modelStateErrors": null
}
```
-----

### Get Message<br>
`Path: /message` <br>
`Method: GET` <br>
`Returns: Message.` <br>
`Success Status Code: 200 OK` <br>

 -*Gets message by messageId.*

| Parameter  | Type |  Default | Desc | 
| ------------ | ------------  | ------------ |  ------------ |
| id  |   int   |   | Message ID to GET. |



#### Success Response
`Status Code: 200 OK`
```json
{
    "data": {
        "id": 1,
        "dateAdded": "2021-03-31T19:34:01.037936",
        "relatedId": 1,
        "fromId": 1,
        "toId": 1,
        "ipAddr": "::1",
        "messageContent": "Hello Worldd",
        "replyCount": 3
    },
    "totalCount": 0,
    "success": true,
    "message": null,
    "exceptionType": null,
    "statusCode": 200,
    "messages": null,
    "modelStateErrors": null
}
```
-----

 ### Message List<br>
`Path: /messsage/list` <br>
`Method: GET` <br>
`Returns: Message list.` <br>
`Success Status Code: 200 OK` <br>

 -*List messages by parameters.*

| Parameter  | Type |  Default | Desc | 
| ------------ | ------------  | ------------ |  ------------ |
| ToId  |   int   | optional  | Filter by ToId. |
| FromId | int | optional | Filter by FromId. |
| RelatedId | int | optional | Filter by RelatedId.  |
| MessageDirection | string  | asc | Direction of the list ("asc" for Ascending or "desc" for Descending).  |
| Term | string | optional | Search term for MessageContent. |
| LastId | int (optional) |  optional  | Filter by records ids greater (default) or less than LastId parameter.  (Related to MessageDirection parameter.) |
| Ip | string  | optional | Filter by IpAdrr.  |
| StartDate| DateTime | optional | Filter records by greater or equal to StartDate.  |
| EndDate| DateTime | optional | Filter records by smaller or equal to EndDate.  |
| ShowDeleted | int | 0 | Show deleted records. (0 = No, 1 = Yes)
| Take | int | optional | Used for take N records. (You can not use it with LastId parameter.)
| Skip | int | optional | Used for skip N records from beginning. (You can not use it with LastId parameter.)

#### Success Response
`Status Code: 200 OK`
```json
{
    "data": [
        {
            "id": 3,
            "dateAdded": "2021-05-01T16:07:25.728154",
            "relatedId": 1,
            "isDeleted": false,
            "fromId": 1,
            "toId": 1,
            "ipAddr": "::1",
            "replyCount": 0,
            "dateLastActivity": "2021-05-01T16:07:25.728154"
        },
        {
            "id": 5,
            "dateAdded": "2021-05-01T16:52:25.866964",
            "relatedId": 1,
            "isDeleted": false,
            "fromId": 1,
            "toId": 1,
            "ipAddr": "::1",
            "replyCount": 0,
            "dateLastActivity": "2021-05-01T16:52:25.866964"
        }
    ],
    "totalCount": 2,
    "success": true,
    "message": null,
    "exceptionType": null,
    "statusCode": 200,
    "messages": null,
    "modelStateErrors": null
}
```
-----

 ### Message Count<br>
`Path: /message/count` <br>
`Method: GET` <br>
`Returns: Message count.` <br>
`Success Status Code: 200 OK` <br>

 -*Gets message count by parameters.*

| Parameter  | Type |  Default | Desc | 
| ------------ | ------------  | ------------ |  ------------ |
| ToId  |   int   | optional  | Filter by ToId. |
| FromId | int | optional | Filter by FromId. |
| RelatedId | int | optional | Filter by RelatedId.  |
| Term | string | optional | Search term for MessageContent. |
| LastId | int (optional) |  optional  | Filter by records ids greater (default) or less than LastId parameter.  (Related to MessageDirection parameter.) |
| Ip | string  | optional | Filter by IpAdrr.  |
| StartDate| DateTime | optional | Filter records by greater or equal to StartDate.  |
| EndDate| DateTime | optional | Filter records by smaller or equal to EndDate.  |
| ShowDeleted | int | 0 | Show deleted records. (0 = No, 1 = Yes)

#### Success Response
`Status Code: 200 OK`
```json
{
    "data": 4,
    "totalCount": 0,
    "success": true,
    "message": null,
    "exceptionType": null,
    "statusCode": 200,
    "messages": null,
    "modelStateErrors": null
}
```
-----

### Get Message Basic Info<br>
`Path: /message` <br>
`Method: GET` <br>
`Returns: Basic info of message.` <br>
`Success Status Code: 200 OK` <br>

 -*Gets message basic info (relatedId, fromId, toId) by messageId.*

| Parameter  | Type |  Default | Desc | 
| ------------ | ------------  | ------------ |  ------------ |
| id  |   int   |   | Message ID to get basic info. |


#### Success Response
`Status Code: 200 OK`
```json
{
    "data": {
        "relatedId": 1,
        "fromId": 1,
        "toId": 1
    },
    "totalCount": 0,
    "success": true,
    "message": null,
    "exceptionType": null,
    "statusCode": 200,
    "messages": null,
    "modelStateErrors": null
}
```
-----
### Get Message Top Senders<br>
`Path: /message/topsenders` <br>
`Method: GET` <br>
`Returns: Message top senders.` <br>
`Success Status Code: 200 OK` <br>

 -*Gets the message top senders with count by parameters.*

| Parameter  | Type |  Default | Desc | 
| ------------ | ------------  | ------------ |  ------------ |
| id  |   int   |   | Message ID to get basic info. |
| Take | int | optional | Used for take N records.
| StartDate| DateTime | optional | Filter records by greater or equal to StartDate.  |
| EndDate| DateTime | optional | Filter records by smaller or equal to EndDate.  |


#### Success Response
`Status Code: 200 OK`
```json
{
    "data": [
        {
            "fromId": 1,
            "count": 3
        },
        {
            "fromId": 2,
            "count": 1
        }
    ],
    "totalCount": 0,
    "success": true,
    "message": null,
    "exceptionType": null,
    "statusCode": 200,
    "messages": null,
    "modelStateErrors": null
}
```
-----

 ### Add Reply<br>
`Path: /reply/` <br>
`Method: POST` <br>
`Content-Type: application/json` <br>
`Returns: The added reply.` <br>
`Success Status Code: 201 Created` <br>

 -*Adds a new reply to message.*

| Property  | Type | Default | Desc |
| ------------ | ------------ |------------ | ------------ |
| MessageId  |     int   |  | Message id to add reply. |
| IsReply | bool | false | IsReply is a bool value to detect if this reply sent from message owner or not.  |
| IpAddr | string  | Request ip address | Ip address of the sender. |
| MessageContent | string |  | Content of the message. |

#### Request Body

```json
{
    "MessageId" : 1,
    "MessageContent" : "Hello from reply",
    "IpAddr": "123.456.789",
    "IsReply": "false"
}
```

#### Success Response
`Status Code: 201 Created`
```json
{
    "data": {
        "id": 5,
        "dateAdded": "2021-06-30T11:38:35.1012827Z",
        "messageId": 1,
        "message": null,
        "isReply": false,
        "messageContent": "Hello from reply",
        "ipAddr": "123.456.789"
    },
    "totalCount": 0,
    "success": true,
    "message": null,
    "exceptionType": null,
    "statusCode": 201,
    "messages": null,
    "modelStateErrors": null
}
```
-----

 ### Reply List<br>
`Path: /reply` <br>
`Method: GET` <br>
`Returns: Reply list.` <br>
`Success Status Code: 200 OK` <br>

 -*List replies by parameters.*

| Parameter  | Type |  Default | Desc | 
| ------------ | ------------  | ------------ |  ------------ |
| ReplyDirection | string  | asc | Direction of the list ("asc" for Ascending or "desc" for Descending).  |
| LastId | int (optional) |  optional  | Filter by records ids greater (default) or less than LastId parameter.  (Related to MessageDirection parameter.) |
| Ip | string  | optional | Filter by IpAdrr.  |
| StartDate| DateTime | optional | Filter records by greater or equal to StartDate.  |
| EndDate| DateTime | optional | Filter records by smaller or equal to EndDate.  |
| ShowDeleted | int | 0 | Show deleted records. (0 = No, 1 = Yes)
| Take | int | optional | Used for take N records. (You can not use it with LastId parameter.)
| Skip | int | optional | Used for skip N records from beginning. (You can not use it with LastId parameter.)

#### Success Response
`Status Code: 200 OK`
```json
{
    "data": [
        {
            "id": 2,
            "messageId": 1,
            "isReply": false,
            "messageContent": "Hello from reply",
            "ipAddr": "::1",
            "dateAdded": "2021-03-31T20:00:33.195343",
            "isDeleted": false
        },
        {
            "id": 3,
            "messageId": 1,
            "isReply": false,
            "messageContent": "Hello from reply",
            "ipAddr": "::1",
            "dateAdded": "2021-03-31T20:01:04.481368",
            "isDeleted": false
        },
        {
            "id": 4,
            "messageId": 1,
            "isReply": false,
            "messageContent": null,
            "ipAddr": "::1",
            "dateAdded": "2021-04-01T13:30:39.121551",
            "isDeleted": true
        },
        {
            "id": 5,
            "messageId": 1,
            "isReply": false,
            "messageContent": "Hello from reply",
            "ipAddr": "123.456.789",
            "dateAdded": "2021-06-30T11:38:35.101282",
            "isDeleted": false
        }
    ],
    "totalCount": 4,
    "success": true,
    "message": null,
    "exceptionType": null,
    "statusCode": 200,
    "messages": null,
    "modelStateErrors": null
}
```
-----

### Delete Reply<br>
`Path: /reply` <br>
`Method: DELETE` <br>
`Returns: Status Code` <br>
`Success Status Code: 200 OK` <br>

 -*Deletes reply  by replyId.*

| Parameter  | Type |  Default | Desc | 
| ------------ | ------------  | ------------ |  ------------ |
| id  |   int   |   | Reply ID to delete. |


#### Success Response
`Status Code: 200 OK`
```json
{
    "success": true,
    "message": null,
    "exceptionType": null,
    "statusCode": 200,
    "messages": null,
    "modelStateErrors": null
}
```
-----

 ### Add Log<br>
`Path: /log/`  <br>
`Method: POST` <br>
`Content-Type: application/json` <br>
`Returns: The added log.` <br>
`Success Status Code: 201 Created` <br>

 -*Adds a new log.*

| Property  | Type |  Desc |
| ------------ | ------------ | ------------ |
| MessageId  |        int   | MessageId to add log. |
| UserId  |     int   | UserId to add log. |
| Action | int  | Log action. |
------------

#### Success Response
`Status Code: 201 Created`
```json
{
    "data": {
        "messageId": 1,
        "action": 1,
        "userId": 0
    },
    "totalCount": 0,
    "success": true,
    "message": null,
    "exceptionType": null,
    "statusCode": 201,
    "messages": null,
    "modelStateErrors": null
}
```
