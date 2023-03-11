import React, { useState, useEffect } from "react";
import { Article } from "../../Models/Article";
import { fetchArticles } from "../../Services/ArticleService/article.services";
import { ArticleTemplate } from "./articleTemplate.component";
import { useAuth0 } from "@auth0/auth0-react";
import { Users } from "../../Models/Users";

export const MainArticles = () => {
  const [articleList, setArticleList] = useState<Article[]>([]);
  const [isLoading, setIsLoading] = useState<boolean>(false);
  const [error, setError] = useState<string>("");
  const { user } = useAuth0();

  const fetchArticleList = async () => {
    setIsLoading(true);
    let currentUser: Users = {
      Id: 0,
      Email: user?.email,
      FirstCategoryID: 0,
      SecondCategoryID: 0,
      ThirdCategoryID: 0,
    };
    try {
      const data = await fetchArticles(currentUser);
      if (data) {
        setArticleList(data);
        setIsLoading(false);
      }
    } catch (error) {
      let errorMessage: string = "Unknown error occurred";
      setError(errorMessage);
      setIsLoading(false);
    }
  };

  useEffect(() => {
    fetchArticleList();
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, []);

  return (
    <div className="articles-container">
      {isLoading ? (
        <p>Loading articles...</p>
      ) : error ? (
        <p>{error}</p>
      ) : (
        articleList.map((article: Article) => (
          <ArticleTemplate key={article.guid} article={article} />
        ))
      )}
    </div>
  );
};
