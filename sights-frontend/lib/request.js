export async function get(url, jwtEncryptedToken = null){
    let _headers = { 'Accept': 'application/json' };

    if (jwtEncryptedToken != null){
        _headers['Authorization'] = `Bearer ${jwtEncryptedToken}`;
    }

    const response = await fetch( url , {
        method: 'GET',
        mode: 'cors',
        headers: _headers,
    }).then(res => res.json());

    return response;
}

export async function post(url, obj, jwtEncryptedToken = null){

    let _headers =  {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
          };

    if (jwtEncryptedToken != null){
        _headers['Authorization'] = `Bearer ${jwtEncryptedToken}`;
    }

    const response = await fetch( url , {
        method: 'POST',
        mode: 'cors',
        headers: _headers,
        body: JSON.stringify(obj)
    })
    const message = await response.json();
    return {status:response.status,response:message};
}

export async function deleteRequest(url, jwtEncryptedToken = null){

        let _headers =  {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
          };

    if (jwtEncryptedToken != null){
        _headers['Authorization'] = `Bearer ${jwtEncryptedToken}`;
    }

    const response = await fetch( url , {
        method: 'DELETE',
        mode: 'cors',
        headers: _headers,
    })
    return response;
}

export async function putRequest(url, obj, jwtEncryptedToken = null){

      let _headers =  {
      'Accept': 'application/json',
      'Content-Type': 'application/json'
    };

    if (jwtEncryptedToken != null){
        _headers['Authorization'] = `Bearer ${jwtEncryptedToken}`;
    }

    const response = await fetch( url , {
        method: 'PUT',
        mode: 'cors',
        headers: _headers,
          body: JSON.stringify(obj)
    }).then(res => res.json())
    return response;
}
