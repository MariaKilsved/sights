export async function get(url){
    const response = await fetch( url , {
        method: 'GET',
        mode: 'cors',
        headers: {
            'Accept': 'application/json'
          },
    }).then(res => res.json())
    return response;
}

