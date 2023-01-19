export async function get(url){
    const response = await fetch( url , {
        method: 'GET',
        
        
    });
    return response.json();
}