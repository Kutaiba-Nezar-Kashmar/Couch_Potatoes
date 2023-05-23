import React, { useEffect } from 'react';
import { useNavigate } from 'react-router-dom';

// NOTE: (mibui 2023-05-19) User will be redirected to this page after authentication
//                          and then we will redirect them from here
const Authenticated = () => {
    const navigate = useNavigate();

    useEffect(() => {
        navigate('/');
    }, []);

    return <div>Authenticated!</div>;
};

export default Authenticated;
