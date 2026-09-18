from fastapi import FastAPI
from fastapi.middleware.cors import CORSMiddleware
from app.db.database import Base, engine
from app.routers.customer import router as customer_router


Base.metadata.create_all(bind=engine)


app = FastAPI(
    title="Customer Service",
    description="Customer registration and authentication service",
    version="1.0.0"
)

# -----------------------------
# CORS Configuration
# -----------------------------
app.add_middleware(
    CORSMiddleware,
    allow_origins=[
        "http://localhost:4200",
        "http://localhost:5000"
    ],
    allow_credentials=True,
    allow_methods=["*"],
    allow_headers=["*"],
)
app.include_router(customer_router)


@app.get("/")
def root():

    return {
        "service": "Customer Service",
        "status": "running"
    }