import { useAuth0 } from "@auth0/auth0-react";
import { useEffect, useState } from "react";
import { Article } from "../../Models/Article";
import { fetchPopularArticles } from "../../Services/ArticleService/article.services";
import { ArticleTemplate } from "../MainArticle/articleTemplate.component";

export const Popular = () => {
  const [articleList, setArticleList] = useState<Article[]>([]);
  const [isLoading, setIsLoading] = useState<boolean>(false);
  const [error, setError] = useState<string>("");
  const { user } = useAuth0();

  const fetchPopularArticleList = async () => {
    setIsLoading(true);
    try {
      let Email: string | undefined = user?.email;
      const data = await fetchPopularArticles(Email);
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
    fetchPopularArticleList();
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, []);

  return (
    <div className="articles-container">
      {isLoading ? (
        <div className="loading">
          <p>Loading articles...</p>
          <p>Please choose 3 categories if you haven't done it.</p>
        </div>
      ) : error ? (
        <p>{error}</p>
      ) : articleList.length === 0 ? ( // Check if the array is empty
        <p>No popular articles found.</p>
      ) : (
        articleList.map((article: Article) => (
          <ArticleTemplate key={article.guid} article={article} />
        ))
      )}
    </div>
  );
};
