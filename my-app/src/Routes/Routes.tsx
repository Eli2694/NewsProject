import { Route, Routes } from "react-router-dom";
import { MainArticles } from "../Components/MainArticle/mainArticles.component";
import { PageNotFound } from "../Components/pageNotFound.component";
import { Categories } from "../Components/SettingPage/categories.component";

function RoutesDom() {
  return (
    <Routes>
      <Route path="/" element={<Categories></Categories>}></Route>
      <Route path="/main" element={<MainArticles></MainArticles>}></Route>
      <Route path="*" element={<PageNotFound></PageNotFound>}></Route>
    </Routes>
  );
}

export default RoutesDom;
