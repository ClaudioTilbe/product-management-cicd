FROM node:22-alpine AS build
WORKDIR /app
COPY package*.json ./
RUN npm ci
COPY . .

# Configuración de Angular a utilizar durante el build.
# Si no se especifica ninguna, utilizará "production".
ARG BUILD_CONFIGURATION=production
RUN npm run build -- --configuration ${BUILD_CONFIGURATION}


FROM nginx:alpine AS runtime
COPY --from=build /app/dist/ProductManagement.UI/browser /usr/share/nginx/html
COPY nginx.conf /etc/nginx/conf.d/default.conf
EXPOSE 80
