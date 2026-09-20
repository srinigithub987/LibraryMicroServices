FROM python:3.10.13-slim

WORKDIR /app

ENV PYTHONDONTWRITEBYTECODE=1
ENV PYTHONUNBUFFERED=1

COPY CustomerService/requirements.txt .

RUN pip install --no-cache-dir -r requirements.txt

COPY CustomerService/app ./app

EXPOSE 8001

CMD ["uvicorn", "app.main:app", "--host", "0.0.0.0", "--port", "8001"]