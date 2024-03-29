import { useAuth0 } from "@auth0/auth0-react";
import React, { useEffect, useState } from "react";
import { Article } from "../../Models/Article";
import { fetchCuriousArticles } from "../../Services/ArticleService/article.services";
import { ArticleTemplate } from "../MainArticle/articleTemplate.component";

export const Curious = () => {
  const [articleList, setArticleList] = useState<Article[]>([]);
  const [isLoading, setIsLoading] = useState<boolean>(false);
  const [error, setError] = useState<string>("");
  const { user } = useAuth0();

  const fetchCuriousArticleList = async () => {
    setIsLoading(true);
    try {
      let Email: string | undefined = user?.email;
      const data = await fetchCuriousArticles(Email);
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
    fetchCuriousArticleList();
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
