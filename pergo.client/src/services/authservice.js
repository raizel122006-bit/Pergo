import Register from "../pages/Register"

const API_URL = 'http://localhost:5041/api/auth'

export async function registerUser(userData){
    try{
        const response = await fetch(`${API_URL}/register`,{
            method: 'POST',
            headers: {
                'Content-Type' : 'application/json',
            },
            body: JSON.stringify(userData),

        }) 

        const data = await response.json()
        if (!response.ok){
            throw new Error(data.message || 'Registration failed')
        }
        return data

    } catch(error){
        throw error
    }
}

export async function loginUser(userData){
    const response = await fetch(`${API_URL}/login`,{
        method: 'POST',
        headers: {
            'Content-Type' : 'application/json',
        },
        body: JSON.stringify(userData),
    });

    const data = await response.json();

    if(!response.ok){
        throw new Error(data.message || 'Login failed');
    }

    return data;

}
