import { useAuth0 } from "@auth0/auth0-react";
import React from "react";
import "./App.css";
import { Login } from "./Components/Login/login.component";
import Logout from "./Components/Logout/logout.component";
import { Menu } from "./Routes/menu.routes";

import RoutesDom from "./Routes/Routes";

function App() {
  const { isAuthenticated, isLoading } = useAuth0();
  if (isLoading) {
    return (
      <div className="App">
        {" "}
        <h3>Loading...</h3>
      </div>
    );
  } else {
    if (isAuthenticated) {
      return (
        <div className="page-wrapper flex flex-column">
          <div className="header-wrapper flex">
            <span className="header-news">News</span>
            <div className="header-title flex-fill-remaining">
              <span>
                <Logout></Logout>
              </span>
            </div>
          </div>
          <div className="menu-content-footer-wrapper flex flex-fill-remaining">
            <div className="menu">
              <Menu></Menu>
            </div>
            <div className="content-footer-wrapper flex flex-column flex-fill-remaining">
              <div className="content flex-fill-remaining">
                <RoutesDom></RoutesDom>
              </div>
              <div className="footer">Footer</div>
            </div>
          </div>
        </div>
      );
    } else {
      return (
        <div className="login">
          <Login></Login>
        </div>
      );
    }
  }
}

export default App;
