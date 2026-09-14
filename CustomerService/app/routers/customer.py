from fastapi import APIRouter, Depends, HTTPException, status
from sqlalchemy.orm import Session

from app.db.database import get_db

from app.schema.customer import (
    CustomerRegister,
    CustomerLogin,
    TokenResponse,
    CustomerResponse
)

from app.services.auth_service import (
    register_customer,
    authenticate_customer,
    generate_customer_token
)


router = APIRouter(
    prefix="/api/customers",
    tags=["Customers"]
)


@router.post(
    "/register",
    response_model=CustomerResponse,
    status_code=status.HTTP_201_CREATED
)
def register(
    request: CustomerRegister,
    db: Session = Depends(get_db)
):

    customer = register_customer(
        db=db,
        first_name=request.first_name,
        last_name=request.last_name,
        email=request.email,
        password=request.password
    )

    if customer is None:

        raise HTTPException(
            status_code=409,
            detail="Email already registered"
        )

    return customer


@router.post(
    "/login",
    response_model=TokenResponse
)
def login(
    request: CustomerLogin,
    db: Session = Depends(get_db)
):

    customer = authenticate_customer(
        db=db,
        email=request.email,
        password=request.password
    )

    if customer is None:

        raise HTTPException(
            status_code=401,
            detail="Invalid email or password"
        )

    token = generate_customer_token(customer)

    return {
        "access_token": token,
        "token_type": "bearer"
    }