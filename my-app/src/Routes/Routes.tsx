import { Route, Routes } from "react-router-dom";
import { Curious } from "../Components/Curious/curious.component";
import { MainArticles } from "../Components/MainArticle/mainArticles.component";
import { PageNotFound } from "../Components/pageNotFound.component";
import { Popular } from "../Components/Popular/popular.component";
import { Categories } from "../Components/SettingPage/categories.component";

function RoutesDom() {
  return (
    <Routes>
      <Route path="/" element={<Categories></Categories>}></Route>
      <Route path="/main" element={<MainArticles></MainArticles>}></Route>
      <Route path="/popular" element={<Popular></Popular>}></Route>
      <Route path="/curious" element={<Curious></Curious>}></Route>
      <Route path="*" element={<PageNotFound></PageNotFound>}></Route>
    </Routes>
  );
}

export default RoutesDom;
