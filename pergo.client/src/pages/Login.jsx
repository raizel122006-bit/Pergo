
import { Link } from 'react-router'
import { useState } from 'react'
import { loginUser } from '../services/authservice';


function Login() {

    const[formData, setFormData] = useState({
        email : '',
        password: ''
    });

    const [message, setMessage] = useState('');

    const handleChange = (e) => {
        setFormData({...formData, [e.target.name]: e.target.value});
    };

    const handleSubmit = async (e) => {
        e.preventDefault();

        try{
            const result = await loginUser(formData);
            localStorage.setItem('token', result.token);
            setMessage('Login successful!');
        } catch(error){
            setMessage(error.message);
        }
    };


  return (
    <div className="min-h-screen bg-slate-50 flex items-center justify-center px-4 py-10">

      <div className="w-full max-w-md">

        {/* Brand */}
        <div className="text-center mb-8">
          <h1 className="text-3xl font-bold text-violet-600">
            Pergo 
          </h1>
        </div>

        {/* Login Card */}
        <div className="bg-white p-8 rounded-2xl shadow-sm border border-slate-200">

          <div className="mb-7">
            <h2 className="text-2xl font-semibold text-slate-900">
              Welcome back
            </h2>
            <p className="text-slate-500 mt-2 text-sm">
              Enter your details to access your account.
            </p>
          </div>

          <form onSubmit={handleSubmit} className="space-y-5">

            <div>
              <label className="block text-sm font-medium text-slate-700 mb-2">
                Email address
              </label>
              <input
                name="email"
                value={formData.email}
                onChange={handleChange}
                type="email"
                placeholder="you@example.com"
                className="w-full px-4 py-3 border border-slate-300 rounded-lg outline-none focus:ring-2 focus:ring-violet-500 focus:border-violet-500"
              />
            </div>

            <div>
              <label className="block text-sm font-medium text-slate-700 mb-2">
                Password
              </label>
              <input
                name="password"
                value={formData.password}
                onChange={handleChange}
                type="password"
                placeholder="Enter your password"
                className="w-full px-4 py-3 border border-slate-300 rounded-lg outline-none focus:ring-2 focus:ring-violet-500 focus:border-violet-500"
              />
            </div>

            <button
              type="submit"
              className="w-full bg-violet-600 text-white py-3 rounded-lg font-medium hover:bg-violet-700 transition-colors"
            >
              Sign in
            </button>

          </form>
            <p>{message}</p>
          <p className="text-center text-sm text-slate-500 mt-7">
            Don't have an account?{' '}
            <Link
              to="/register"
              className="text-violet-600 font-semibold hover:text-violet-700"
            >
              Sign up
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

export default Login
