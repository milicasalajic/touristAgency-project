import React from 'react';
import { BrowserRouter as Router, Route, Routes } from 'react-router-dom';
import Home from './components/Home/Home';
import TouristPackageDetail from './components/TouristPackage/TouristPackage';
import Login from './components/Login/Login'; 
import Navbar from './components/Navbar/Navbar';
import MyProfile from "./components/MyProfile/MyProfile";
import Registration from './components/Registration/Registration';
import { TouristPackagesGroup } from './model/TouristPackages';

function App() {
  return (
    <Router>
      <Navbar onCategorySelect={function (selectedPackages: TouristPackagesGroup[]): void {
        throw new Error('Function not implemented.');
      } } />  
      <div className="App">
        <Routes>
          <Route path="/" element={<Home />} />
          <Route path="/tourist-package/:id" element={<TouristPackageDetail />} />
          <Route path="/login" element={<Login />} /> 
          <Route path="/profile" element={<MyProfile />} />
          <Route path="/registration" element={<Registration />} />
        </Routes>
      </div>
    </Router>
  );
}

export default App;

