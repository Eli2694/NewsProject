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
      id: 0,
      email: user?.email,
      firstCategoryID: 0,
      secondCategoryID: 0,
      thirdCategoryID: 0,
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
        <div className="loading">
          <p>Loading articles...</p>
          <p>Please Choose 3 Categories!</p>
        </div>
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