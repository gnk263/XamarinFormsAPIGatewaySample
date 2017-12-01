import json

print('Loading function')

def lambda_handler(event, context):

    body = json.loads(event['body'])

    print(body)

    response = []

    for param in body:
        data = {}
        data['Id'] = param['Id']
        data['Sum'] = param['Param1'] + param['Param2']
        response.append(data)


    return {
        'statusCode': '200',
        'headers': {
            'Content-Type': 'application/json',
        },
        'body': json.dumps(response),
    }
    