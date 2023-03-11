import React from "react";
import { Link } from "react-router-dom";
import "./menu.css";

export const Menu = () => {
  return (
    <div className="menu-container">
      <div className="menu-title">Menu</div>
      <Link to="/" className="link">
        <button className="menu-btn">Setting</button>
      </Link>
      <Link to="/Main" className="link">
        <button className="menu-btn">Main</button>
      </Link>
    </div>
  );
};
