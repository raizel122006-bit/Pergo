
import { Link } from 'react-router'
import { useState } from 'react'
import { registerUser } from '../services/authservice'


function Register() {

    //Form state
    const [formData, setFormData] = useState({
        fullName: '',
        email: '',
        password: ''
    })

    const [message, setMessage] = useState('');

    //Change handler
    const handleChange = (e) => {
        setFormData({...formData, [e.target.name]: e.target.value})
    }

    //Submit handler
    const handleSubmit = async (e) => {
        e.preventDefault();

        try{
            const result = await registerUser(formData);
            localStorage.setItem('token', result.token);
            setMessage('Account created successfully');
        } catch(error){
            setMessage(error.message);
        }
    }

    return (

        <div className="min-h-screen bg-slate-50 flex items-center justify-center px-4 py-10">

        <div className="w-full max-w-md">

            {/* Brand */}
            <div className="text-center mb-8">
            <h1 className="text-3xl font-bold text-violet-600">
                Pergo
            </h1>
            
            </div>

            {/* Registration Card */}
            <div className="bg-white p-8 rounded-2xl shadow-sm border border-slate-200">

            <div className="mb-7">
                <h2 className="text-2xl font-semibold text-slate-900">
                Create an account
                </h2>
            </div>

            <form onSubmit={handleSubmit} className="space-y-5">

                <div>
                <label className="block text-sm font-medium text-slate-700 mb-2">
                    Full name
                </label>
                <input
                    type="text"
                    name="fullName"
                    value={formData.fullName}
                    onChange={handleChange}
                    />
                </div>

                <div>
                <label className="block text-sm font-medium text-slate-700 mb-2">
                    Email address
                </label>
                <input
                    type="email"
                    name="email"
                    value={formData.email}
                    onChange={handleChange}
                    />
                </div>

                <div>
                <label className="block text-sm font-medium text-slate-700 mb-2">
                    Password
                </label>
                <input
                    type="password"
                    name="password"
                    value={formData.password}
                    onChange={handleChange}
                    />
                </div>

                <button
                type="submit"
                className="w-full bg-violet-600 text-white py-3 rounded-lg font-medium hover:bg-violet-700 transition-colors"
                >
                Create account
                </button>

            </form>
            <p>{message}</p>
            <p className="text-center text-sm text-slate-500 mt-7">
                Already have an account?{' '}
                <Link
                to="/login"
                className="text-violet-600 font-semibold hover:text-violet-700"
                >
                Sign in
                </Link>
            </p>

            </div>

            <p className="text-center text-xs text-slate-400 mt-6">
            © 2026 Pergo. All rights reserved.
            </p>

        </div>
        </div>
    )
    }

export default Register
