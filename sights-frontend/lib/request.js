export async function get(url){
    const response = await fetch( url , {
        method: 'GET',
        mode: 'cors',
        headers: {
            'Accept': 'application/json',
          },
    }).then(res => res.json());
    return response;
}

export async function post(url, obj){
    const response = await fetch( url , {
        method: 'POST',
        mode: 'cors',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
          },
          body: JSON.stringify(obj)
    }).then(res => res.json())
    return response;
}

export async function deleteRequest(url, obj){
    const response = await fetch( url , {
        method: 'DELETE',
        mode: 'cors',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
          },
          body: JSON.stringify(obj)
    }).then(res => res.json())
    return response;
}

export async function putRequest(url, obj){
    const response = await fetch( url , {
        method: 'PUT',
        mode: 'cors',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
          },
          body: JSON.stringify(obj)
    }).then(res => res.json())
    return response;
}
