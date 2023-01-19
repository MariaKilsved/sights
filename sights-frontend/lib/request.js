export async function get(url){
    const response = await fetch( url , {
        method: 'GET',
        mode: 'cors',
        
    });
    return response.json();
}